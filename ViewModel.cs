using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;



namespace SQLTool
{
    public class TableToDB
    {

        [Display(Name = "Table名稱")]
        public string Table_Name { get; set;}

        [Display(Name = "Where條件")]
        public string Where { get; set; }

        public TableToDB()
        {
        }
    }
}

