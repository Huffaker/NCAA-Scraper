using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WatiN.Core;

namespace NCAA_Scraper
{
	public abstract class Scraper
	{
		protected string scrapResult;
		protected string url;
		protected string javascriptCode;
		private event EventHandler ThreadDone;

		public void RunScrap()
		{
            var newThread = new Thread(ScrapInit);
			newThread.SetApartmentState(ApartmentState.STA);
			ThreadDone += ProcessResult;
			newThread.Start();
		}

		private void ScrapInit()
		{
			scrapResult = AquireText(url, javascriptCode);
			ThreadDone?.Invoke(this, EventArgs.Empty);
		}

		private string AquireText(string strURL, string javascriptCode)
		{
			// Create an instance of IE browser 
			IE browser = new IE(strURL);
			// Wait for it to load (otherwise we don't have jquery)
			browser.WaitForComplete();
			//Get text
			var result = browser.Eval(javascriptCode);
			browser.Close();
			return result;
		}

		protected abstract void ProcessResult(object sender, EventArgs e);
	}
}
