using React;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(webUI.ReactConfig), "Configure")]

namespace webUI
{
	public static class ReactConfig
	{
		public static void Configure()
		{
			// ES6 features are enabled by default. Uncomment the below line to disable them.
			// See http://reactjs.net/guides/es6.html for more information.
			//ReactSiteConfiguration.Configuration.SetUseHarmony(false);

			// Uncomment the below line if you are using Flow
			// See http://reactjs.net/guides/flow.html for more information.
			//ReactSiteConfiguration.Configuration.SetStripTypes(true);

			// If you want to use server-side rendering of React components, 
			// add all the necessary JavaScript files here. This includes 
			// your components as well as all of their dependencies.
			// See http://reactjs.net/ for more information. Example:
            ReactSiteConfiguration.Configuration                
                .AddScript("~/Scripts/internal/eus/eusAPI.js")              // API calls
                .AddScript("~/Scripts/internal/eus/eusCommon.js")           // Common JS functions
                .AddScript("~/Scripts/internal/eusVote/TopicAnswers.jsx")   // React topic page
                .AddScript("~/Scripts/internal/eusVote/Comments.jsx");	    // React comments page		
		}
	}
}