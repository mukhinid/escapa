using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ExceptionHitBuilder : HitBuilder<ExceptionHitBuilder>
{
    private string exceptionDescription = "";
    private bool fatal = false;

    public string GetExceptionDescription()
    {
        return exceptionDescription;
    }

    public ExceptionHitBuilder SetExceptionDescription(string exceptionDescription)
    {
        if (exceptionDescription != null)
            this.exceptionDescription = exceptionDescription;

        return this;
    }

    public bool IsFatal()
    {
        return fatal;
    }

    public ExceptionHitBuilder SetFatal(bool fatal)
    {
        this.fatal = fatal;
        return this;
    }

    public override ExceptionHitBuilder GetThis()
    {
        return this;
    }

    public override ExceptionHitBuilder Validate()
    {
        return this;
    }
}
