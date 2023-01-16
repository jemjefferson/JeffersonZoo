using System;
using Utilities;

namespace CagedItems
{
    public interface ICageable
    {
        double DisplaySize { get; }

        string ResourceKey { get; }

        int XPosition { get; }

        int YPosition { get; }

        HorizontalDirection XDirection { get; }

        VerticalDirection YDirection { get; }

        HungerState HungerState { get; }

        Action<ICageable> OnImageUpdate { get; set; }

        bool IsActive { get; }
    }
}
