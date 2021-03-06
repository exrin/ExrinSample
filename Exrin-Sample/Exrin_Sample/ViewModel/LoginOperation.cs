﻿namespace ExrinSample.ViewModel
{
    using Abstraction.Enums;
    using Abstraction.Enums.Views;
    using Abstraction.Model;
    using Exrin.Abstraction;
    using Exrin.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoginOperation : ISingleOperation
    {
        private readonly IAuthModel _authModel;

        public LoginOperation(IAuthModel authModel)
        {
            _authModel = authModel;
        }

        public Func<object, CancellationToken, Task<IList<IResult>>> Function
        {
            get
            {
                return async (parameter, token) =>
                {
                    Result result = null;

                    if (await _authModel.Login())
                        result = new Result() { ResultAction = ResultType.Navigation, Arguments = new NavigationArgs() { Key = Main.Main, StackType = Stack.Main } };
                    // Or - result = new NavigationResult(Stack.Main, Main.Main);
                    else
                        result = new Result() { ResultAction = ResultType.Display, Arguments = new DisplayArgs() { Message = "Login was unsuccessful" } };
                    List<IResult> list = result;
                    return list;
                };
            }
        }

        public bool ChainedRollback { get; private set; } = false;

        public Func<Task> Rollback { get { return null; } }
    }
}
