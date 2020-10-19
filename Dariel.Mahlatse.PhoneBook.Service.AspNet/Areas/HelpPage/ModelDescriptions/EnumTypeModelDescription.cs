using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dariel.Mahlatse.PhoneBook.Service.AspNet.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}