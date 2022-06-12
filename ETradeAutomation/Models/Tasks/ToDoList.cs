using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcOnlineTicariOtomasyon.Models.Tasks
{
    public class ToDoList
    {
        public int Id { get; set; }
        [Column(TypeName="nvarchar")]
        [StringLength(100)]
        public string Title { get; set; }

        [Column(TypeName = "bit")]
        public bool Status { get; set; }

    }
}
