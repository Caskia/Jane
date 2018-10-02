﻿using Jane.Dependency;

namespace Jane.Runtime.Guids
{
    public class SequentialGuidGeneratorOptions
    {
        /// <summary>
        /// Default value: <see cref="SequentialGuidType.SequentialAtEnd"/>.
        /// </summary>
        public SequentialGuidType DefaultSequentialGuidType { get; set; }

        public SequentialGuidGeneratorOptions()
        {
            DefaultSequentialGuidType = SequentialGuidType.SequentialAtEnd;
        }
    }
}