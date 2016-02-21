# NCAA-Scraper
### Loads NCAA stats into SQL server using a C# console application

Simple application that kicks up a [PhantomJS](http://phantomjs.org/) browser, navigates to the data source and scraps the data using [JQuery](https://jquery.com/). Why JQuery? Because it's WAY easier to parse html with JQuery then it is with the string library in C#.

Once the data is in C#, its easy to load into SQL using [SqlBulkCopy](https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlbulkcopy(v=vs.110).aspx). Additionally, this application using a few merge statements to load partial data scraps. Data is saved into SQL as it runs to minimize the impact of any errors and crashing while scraping data.

Currently, the application is setup to pull Player stats per individual game. You can then aggregate the data however you wish from there.
