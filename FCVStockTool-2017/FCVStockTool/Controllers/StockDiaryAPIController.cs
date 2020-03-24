using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FCVStockTool.Controllers
{
    public class StockDiaryAPIController : ApiController
    {
        StockDbContext db =  new StockDbContext();
        
        public IEnumerable<StockDiary> Get()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.StockDiaries.AsEnumerable();
        }
    }
}
