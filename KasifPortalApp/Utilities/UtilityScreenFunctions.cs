using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KasifPortalApp.Utilities
{
    public class UtilityScreenFunctions
    {

        //public string ManipulateDataItemValue()
        //{

        //}

        public enum ResultStatus
        {
            Success = 1,
            Error = -1
        }
    }
    public class NameValue
    {
        private string _Name;
        private string _Value;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }

}