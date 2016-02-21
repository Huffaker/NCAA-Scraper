using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NReco.PhantomJS;

namespace NCAA_Scraper
{
	public abstract class Scraper
	{
		protected string scrapResult { get; set; }
		protected string javascriptCode { get; set; }
		private PhantomJS browser;
		private int count = 0;
		protected int totalCalls = 0;

		public void RunScrap(string url)
		{
			// Create an instance of Phantom browser 
			if (browser == null)
				browser = new PhantomJS();
			scrapResult = AquireText(url, javascriptCode);
			ProcessResult(url);
		}

		private string AquireText(string strURL, string javascriptCode)
		{
			using (var ms = new MemoryStream())
			{
				browser.RunScript(@"
var system = require('system');
var page = require('webpage').create();
page.open('" + strURL + @"', function(status) {
	if(status === 'success') {
		var data = page.evaluate(function() {
				" + javascriptCode + @"
		});
		system.stdout.writeLine('!~!' + data + '!~!');
	}
	phantom.exit();
});", null, null, ms);
				ms.Position = 0;
				var sr = new StreamReader(ms);
				var result = sr.ReadToEnd();
				result = result.Split(new [] { "!~!"}, StringSplitOptions.None)[1];
				return result;
			}
		}

		protected abstract void ProcessResult(string url);

		protected void CleanUpBrowser()
		{
			browser.Abort();
		}

		protected void LogResult(string url, int results)
		{
			count++;
            Console.WriteLine(DateTime.Now.ToLongTimeString() + ", URL: " + url + ", Data: " + results + ", Count: " + count + "/" + totalCalls);
		}
	}
}
