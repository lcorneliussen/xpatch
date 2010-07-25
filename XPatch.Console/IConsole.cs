﻿namespace XPatch.Console
{
    public interface IConsole
    {
        string Prompt(string message);
        void Error(string errorMessage);
        void Info(string message);
    }
}