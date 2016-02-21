# NCAA-Scraper
### Loads NCAA stats into SQL server using a C# console application

Simple application that kicks up a [PhantomJS](http://phantomjs.org/) browser, navigates to the data source and scraps the data using [JQuery](https://jquery.com/). Why JQuery? Because it's WAY easier to parse html with JQuery then it is with the string library in C#.

Once the data is in C#, its easy to load into SQL using [SqlBulkCopy](https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlbulkcopy(v=vs.110).aspx). Additionally, this application using a few SQL merge procedures for loading partial data sets. Data is saved into SQL as it runs to minimize the impact of any errors and crashing while scraping data.

Currently, the application is setup to pull Player performance per individual Game as far back as the 2009-2010 season. Changing this to pull at the team level will certainly speed up the data pull (a lot less data), feel free to Fork the project and let me know how it goes!
