using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frame.Sample
{
    public class TreeModel
    {
        public Guid id { get;  set; }
        public Guid parentId { get;  set; }
        public string NodeName { get;  set; }
        public string NodeValue { get;  set; }
        public List<TreeModel> NodeItems { get;  set; }
    
    }
}