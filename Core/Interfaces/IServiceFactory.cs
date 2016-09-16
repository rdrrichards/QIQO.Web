﻿namespace Core
{
    public interface IServiceFactory
    {
        T CreateClient<T>() where T : IServiceContract;
    }
}
