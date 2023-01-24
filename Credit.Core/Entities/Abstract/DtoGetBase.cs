﻿using Credit.Core.Utilities.Results.ComplexTypes;

namespace Credit.Core.Entities.Abstract
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string? Message { get; set; }
    }
}
