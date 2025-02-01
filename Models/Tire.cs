using Microsoft.AspNetCore.SignalR.Protocol;
using TireDrift.Models.ViewModels;

namespace TireDrift.Models
{
    public class Tire : Service
    {
        public int Stock { get; set; }
        public string ImagePath { get; set; }
    }
}