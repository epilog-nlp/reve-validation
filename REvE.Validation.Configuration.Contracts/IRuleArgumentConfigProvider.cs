/*
This source file is under MIT License (MIT)
Copyright (c) 2019 Ian Schlarman
https://opensource.org/licenses/MIT
*/

namespace REvE.Validation.Configuration.Contracts
{
    using Models;

    /// <summary>
    /// <see cref="REvE.Configuration.IConfigProvider{TResult}"/> closed on <see cref="RuleArgumentConfig"/>.
    /// </summary>
    public interface IRuleArgumentConfigProvider : REvE.Configuration.IConfigProvider<RuleArgumentConfig>
    {
        
    }
}
