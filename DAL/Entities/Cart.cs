using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Cart : BaseEntity
    {

        //  public int Id { get; set; }
        public int UserId { get; set; }//Kulanıcı bilgilerini
        public int BookId { get; set; }
        public DateTime AddTime { get; set; }
        public bool IsActive { get; set; }


    }
}
