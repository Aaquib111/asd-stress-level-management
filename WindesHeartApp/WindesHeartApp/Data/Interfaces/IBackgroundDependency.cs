using System.Collections.Generic;
using WindesHeartApp.Models;

public interface IBackgroundDependency
{
    void ExecuteCommand(string fileName, IEnumerable<Heartrate> heartrates);
}