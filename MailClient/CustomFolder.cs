using System;

namespace MailClient
{
    public class CustomFolder
    {
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