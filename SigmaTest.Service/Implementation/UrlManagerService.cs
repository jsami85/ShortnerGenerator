using AutoMapper;
using SigmaTest.Domain.Entities;
using SigmaTest.DataAccess;
using SigmaTest.Service.Contract;
using SigmaTest.Service.Exceptions;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SigmaTest.Service.Implementation
{
    public class UrlManagerService : IUrlManagerService
	{ 
		public Task<string> ShortenUrl(string mainUrl, string ip)
		{
			return Task.Run(() =>
			{
				using (var ctx = new ApplicationDbContext())
				{
					var shortner = ctx.Shortner.Where(u => u.LongUrl == mainUrl && u.Ip==ip).FirstOrDefault();
					if (shortner != null)
					{
						return shortner.ShortenedUrl;
					}

					if (!mainUrl.StartsWith("http://") && !mainUrl.StartsWith("https://"))
					{
						throw new ArgumentException("Invalid URL format");
					}
					Uri urlCheck = new Uri(mainUrl);
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCheck);
					request.Timeout = 10000;
					try
					{
						HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					}
					catch (Exception)
					{
						throw new ShortnrNotFoundException();
					}

					//shortner = new Shortner
					//{
					//	LongUrl = mainUrl,
					//	NumOfClicks = 0,
					//	ShortenedUrl = Guid.NewGuid().ToString()
					//};
					return Guid.NewGuid().ToString();
				}
			});
		}

	}
}