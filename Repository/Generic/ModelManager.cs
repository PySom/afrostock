using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AfrroStock.Data;
using AfrroStock.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using AutoMapper;

namespace AfrroStock.Repository.Generic
{
    public class ModelManager<TModel> : IModelManager<TModel>
        where TModel : class, IModel
    {
        private readonly ApplicationDbContext _ctx;

        public ModelManager()
        {

        }
        public ModelManager(ApplicationDbContext context)
        {
            _ctx = context;
        }

        public virtual async ValueTask<(bool, TModel, string)> Add(TModel model)
        {
            string errorMessage = null;
            try
            {
                await _ctx.Set<TModel>().AddAsync(model);
                _ctx.SaveChanges();
                return (true, model, errorMessage);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                errorMessage = ex.Message;
            }
            catch (DbUpdateException ex)
            {
                errorMessage = ex.Message;
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (false, null, errorMessage);
        }

        public void UpdatePartly(TModel model)
        {
            _ctx.Entry<TModel>(model).State = EntityState.Modified;
        }

        public virtual async ValueTask<(bool, TModel, string)> Update(TModel model)
        {
            string errorMessage = null; 
            _ctx.Entry<TModel>(model).State = EntityState.Modified;
            try
            {
                await _ctx.SaveChangesAsync();
                return (true, model, errorMessage);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                errorMessage = ex.Message;
            }
            catch (DbUpdateException ex)
            {
                errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (false, null, errorMessage);
        }

        public async ValueTask<bool> IncreaseView<TI>(TI model) where TI : TModel, IIncrease
        {
            try
            {
                model.Views += 1;
                await Update(model);
                return true;
            }
            catch (Exception ex)
            {
                var _ = ex.Message;
                return false;
            }

        }
        public async ValueTask<(bool, ICollection<TModel>, string)> Add(IEnumerable<TModel> models)
        {
            string errorMessage;
            try
            {
                await _ctx.Set<TModel>().AddRangeAsync(models);
                await _ctx.SaveChangesAsync();
                return (true, models.ToList(), null);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                errorMessage = ex.Message;
            }
            catch (DbUpdateException ex)
            {
                errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (false, null, errorMessage);
        }

        public virtual async ValueTask<(bool, string)> Delete(TModel model)
        {
            _ctx.Entry<TModel>(model).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        public virtual async ValueTask<(bool, string)> Delete(IEnumerable<TModel> models)
        {
            foreach(var model in models)
            {
                _ctx.Entry<TModel>(model).State = EntityState.Deleted;
            }
            return await SaveChangesAsync();
        }
        public async ValueTask<TModel> FindOne(Expression<Func<TModel, bool>> query) => await _ctx.Set<TModel>().Where(query).AsNoTracking().FirstOrDefaultAsync();

        public async ValueTask<ICollection<TModel>> FindMany(Expression<Func<TModel, bool>> query) => await _ctx.Set<TModel>().Where(query).AsNoTracking().ToListAsync();

        public async Task<(bool, string)> SaveChangesAsync()
        {
            string errorMessage;
            try
            {
                await _ctx.SaveChangesAsync();
                return (true, null);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                errorMessage = ex.Message + " " + ex.InnerException.Message;
            }
            catch (DbUpdateException ex)
            {
                errorMessage = ex.Message + " " + ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " " + ex.InnerException.Message;
            }
            return (false, errorMessage);
        }

        public virtual DbSet<TModel> Item() => _ctx.Set<TModel>();
    }

    
    
}
