using SourceBindingTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBindingTest.Selectors
{
    public class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate OddTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var model = item as ItemModel;

            if (model is not null && model.Id % 2 == 0)
            {
                return EvenTemplate;
            }

            return OddTemplate;

        }
    }
}
