using AfrroStock.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace AfrroStock.Repository.Generic
{
    public interface IModelManager<TModel> 
        where TModel: class, IModel
    {
        ValueTask<(bool, TModel, string)> Add(TModel model);
        ValueTask<(bool, ICollection<TModel>, string)> Add(IEnumerable<TModel> models);
        ValueTask<(bool, TModel, string)> Update(TModel model);
        void UpdatePartly(TModel model);
        Task<(bool, string)> SaveChangesAsync();

        ValueTask<(bool, string)> Delete(TModel model);
        ValueTask<(bool, string)> Delete(IEnumerable<TModel> model);

        ValueTask<TModel> FindOne(Expression<Func<TModel, bool>> query);
        ValueTask<ICollection<TModel>> FindMany(Expression<Func<TModel, bool>> query);
        ValueTask<bool> IncreaseView<TI>(TI model) where TI : TModel, IIncrease;
        DbSet<TModel> Item();
    }
}
