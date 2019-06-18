using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Models
{
    public class CustomFolder
    {
        public int Id { get; set; }
        public string FolderName { get; set; }
        private int _itemCount;
        public int ItemCount
        {
            get
            {
                return _itemCount;
            }
            set
            {
                _itemCount = value;
                DateModified = DateTime.Now;
            }
        }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
