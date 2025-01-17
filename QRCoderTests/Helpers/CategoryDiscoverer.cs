﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;
#if !NET35 && !NET452
#endif

namespace QRCoderTests.Helpers
{
#if NET35 || NET452
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CategoryAttribute : Attribute
    {
        public CategoryAttribute(string category) { }
    }
#else
    public class CategoryDiscoverer : ITraitDiscoverer
    {
        public const string KEY = "Category";

        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var ctorArgs = traitAttribute.GetConstructorArguments().ToList();
            yield return new KeyValuePair<string, string>(KEY, ctorArgs[0].ToString());
        }
    }

    //NOTICE: Take a note that you must provide appropriate namespace here
    [TraitDiscoverer("QRCoderTests.XUnitExtenstions.CategoryDiscoverer", "QRCoderTests")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CategoryAttribute : Attribute, ITraitAttribute
    {
        public CategoryAttribute(string category) { }
    }
#endif
}
