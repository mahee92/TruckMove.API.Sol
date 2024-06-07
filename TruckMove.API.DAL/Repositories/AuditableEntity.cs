//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TruckMove.API.DAL.Models;

//namespace TruckMove.API.DAL.Repositories
//{
//    public abstract class AuditableEntity
//    {


//        public int? UpdatedById { get; set; }

//        [ForeignKey("UpdatedById")]
//        public virtual UserModel? UpdatedBy { get; set; }

//        public DateTime? CreatedDate { get; set; }
//        public DateTime? LastModifiedDate { get; set; }
//        public int? CreatedById { get; set; }

//        [ForeignKey("CreatedById")]
//        public virtual UserModel? CreatedBy { get; set; }
//    }
//}
