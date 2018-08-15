﻿namespace HashUtility.Services
{
    /// <summary>
    /// SHA512雜湊
    /// </summary>
    public class Sha256HashTool : BaseHashTool
    {
        public override string HashType { get; } = "SHA256";
    }
}