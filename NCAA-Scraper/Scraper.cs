using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WatiN.Core;

namespace NCAA_Scraper
{
	public abstract class Scraper
	{
		protected string scrapResult { get; set; }
		protected string javascriptCode { get; set; }
		private IE browser;

		public void RunScrap(string url)
		{
			// Create an instance of IE browser 
			if (browser == null)
				browser = new IE();
			scrapResult = AquireText(url, javascriptCode);
			ProcessResult();
		}

		private string AquireText(string strURL, string javascriptCode)
		{
			//Set browser location
			browser.GoToNoWait(strURL);
			// Wait for it to load (otherwise we don't have jquery)
			browser.WaitForComplete();
			//Get text
			browser.RunScript(javascriptCode);
			var result = browser.Text;
			return result;
		}

		protected abstract void ProcessResult();

		protected void CleanUpBrowser()
		{
			browser.Close();
		}
	}
}
