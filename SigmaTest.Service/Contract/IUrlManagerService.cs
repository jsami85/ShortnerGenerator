using SigmaTest.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SigmaTest.Service.Contract
{
    public interface IUrlManagerService
    {
        Task<string> ShortenUrl(string mainUrl, string ip);
    }
}
