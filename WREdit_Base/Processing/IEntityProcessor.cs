﻿using WREdit.Base.Entities;
using WREdit.Base.Processing.Properties;

namespace WREdit.Base.Processing
{
    public interface IEntityProcessor
    {
        void Execute(IEntity entity);
        void RegisterProperties(ICollection<IProcessorProperty> properties);
    }
}