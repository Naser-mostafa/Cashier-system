using CadeDateACcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CafeDateAccess
{
    public class ClsSales 
    {
        public  class SalesDetails
        {
            public string اسم_المنتج { set; get; }
            public short الكميه { set; get; }
            public float الاجمالي { set; get; }
            public float السعر { set; get; }
            public DateTime التاريخ { set; get; }
              public    SalesDetails(string DrinkId,float Price,short Quantity,short TotalPrice,DateTime dt)
            {
                this.اسم_المنتج = DrinkId;
                this.السعر = Price;
                this.الكميه = Quantity;
                this.الاجمالي = TotalPrice;
                this.التاريخ = dt;
            }
        }

        public static Dictionary<byte, int> TableAndTheMultiBill = new Dictionary<byte, int>();
        public class delviryDetails
        {
            public string ClientPhone { get; set; }
            public string DelviryGuyName { get; set; }
            public string ClientAddress { get; set; }
     
        }
        public static delviryDetails CurrentDeliveryDetails = new delviryDetails();

        public static  List<SalesDetails> TotalSales = new List<SalesDetails>();


        public static StringBuilder purchasesBuilder = new StringBuilder();

        public static byte counter = 1;



    }

}
