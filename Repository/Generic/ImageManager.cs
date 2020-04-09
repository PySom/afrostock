using AfrroStock.Data;
using AfrroStock.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfrroStock.Repository.Generic
{
    public class ImageManager : ModelManager<Image>
    {

        public ImageManager()
        {

        }
        public ImageManager(ApplicationDbContext context) : base(context)
        {}

        public async ValueTask<bool> IncreaseView(Image model)
        {
            try
            {
                model.Views += 1;
                await Update(model);
                return true;
            }
            catch(Exception ex)
            {
                var _ = ex.Message;
                return false;
            }
            
        }
    }
}
