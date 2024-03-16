using System.ComponentModel.DataAnnotations;

namespace API_HotelManagement.Data.Data.Entitys
{
    public class EntityBase
    {
        /// <summary>
        /// ID 
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người Tạo 
        /// </summary>
        public Guid CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public Guid ModifiedBy { get; set; }


        /////<summary>
        ///// An integer with the user id of the assigned user for the object
        ///// 
        /////</summary>
        //public int AssignedUserId { get; set; }

        /////<summary>
        ///// The ModuleId of where the object was created and gets displayed
        ///// 
        /////</summary>
        //public int ModuleId { get; set; }

        /////<summary>
        ///// An integer for the user id of the user who created the object
        ///// 
        /////</summary>
        //public int CreatedByUserId { get; set; }

        /////<summary>
        ///// An integer for the user id of the user who last updated the object
        ///// 
        /////</summary>
        //public int LastModifiedByUserId { get; set; }

        /////<summary>
        ///// The date the object was created
        ///// 
        /////</summary>
        //public DateTime CreatedOnDate { get; set; }

        /////<summary>
        ///// The date the object was updated
        ///// 
        /////</summary>
        //public DateTime LastModifiedOnDate { get; set; }

    }
}
