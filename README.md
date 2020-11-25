# VueReddit

**VueReddit** is a simple tool for browsing your favourite **[Reddit](https://www.reddit.com/)** subs.

See it running here: https://vuereddit.azurewebsites.net/

## Features

* Integrate with Reddit via their API to retrieve your favourite subreddits and browse the latest posts therein.

* Uses a masonry style, variable height, reactive layout

* Infinite scroll

* Front end framework provided by **[VueJS](https://vuejs.org/)**

* **[ASP.NET Core 3.1](https://github.com/dotnet/aspnetcore)** for data access and API layers

* Login and integration to Reddit via OAuth2 challenge

## Known issues & future enhancements

* Infinite scroll feature currently capped at 100 posts on screen at any one time. Optimize and increase this.
* Reddit oauth challenge processed up by the Reddit app, if installed, on Android devices with mixed results. Is intermittent, Investigation required
* Safari on iPhone/iPad zooms into the viewport very slightly when textboxes are selected. This makes the sides of the posts sppear to clip unless the viewport is 'pinched' back out again.

## Setting up to run in DEV

 1. Open up the .sln file 
 2. Build all the projects
 3. Open a command prompt at the subdirectory of the Vue application (cpb-redditVue.app\clientapp)
 4. Run ```npm build``` to download Vue and all of the js dependencies
 5. Set the active project to cpb-redditVue.app project, run, and browse to the root URL

 N.B.: Please note that to get oauth to work locally, a custom appId / secret will need to be generated on the Reddit site, and saved to appsettings.json

## Running Tests

1. Backend API Tests - .NET Core (NUnit) - contained within cpb-redditVue.tests - run from Visual Studio Test Explorer
2. Front End Vue / Javascipt tests (Jest) - contained within cpb-redditVue.app\clientapp\tests\ - run from command line by executing ```npm test``` from the cpb-redditVue.app\clientapp folder.
