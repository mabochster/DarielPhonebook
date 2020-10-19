using System;
using System.Reflection;

namespace Dariel.Mahlatse.PhoneBook.Service.AspNet.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}