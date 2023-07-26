﻿using EntityGenerator.CodeGeneration.Languages.Enums;
using EntityGenerator.Core.Models.Enums;
using EntityGenerator.Core.Models.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator.CodeGeneration.Languages
{
  public abstract class CodeLanguageBase : LanguageBase
  {
    protected CodeLanguageBase(StringBuilder sb) : base(sb)
    {
    }
  }
}