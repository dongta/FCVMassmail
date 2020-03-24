using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FCVStockTool.Utils
{
    public static class AutoNumberHelper
    {
        public static string GenerateNumber(ObjectType type, StockDbContext db)
        { 
            int y = DateTime.Now.Year;
            var obj = db.AutoNumbers.SingleOrDefault(p => p.Year == y && p.Object.Equals(type.ToString(), StringComparison.OrdinalIgnoreCase));
            if (obj != null)
            {
                obj.Number++;
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                obj = new AutoNumber()
                {
                    Number = 1,
                    Object = type.ToString(),
                    Year = y,
                };
                db.AutoNumbers.Add(obj);
            }
            db.SaveChanges();
            
            return string.Format("{0}{1:00000}", EnumHelper.GetDescription(type), obj.Number);
        }
       
    }
    public enum ObjectType
    {
        [Description("C")]
        Category,
        [Description("P")]
        Product,
        [Description("D")]
        Department,
        [Description("E")]
        Employee,
        [Description("S")]
        Supplier,
        [Description("R")]
        StockDiary,
        [Description("T")]
        ProductTicket,
        [Description("G")]
        ProductClass,
        [Description("B")]
        Building
    }
}