using System.Web.Mvc;

namespace webUI.Areas.eusVote
{
    public class eusVoteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "eusVote";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    name: null,
            //    url: "topic/contact",
            //    defaults: new { controller = "Home", action = "Contact" },
            //    namespaces: new[] { "webUI.Controllers" }
            //);     

            context.MapRoute(
                name: null,
                url: "eusVote/topic",
                defaults: new { controller = "Topic", action = "Index" },
                namespaces: new[] { "webUI.Areas.eusVote.Controllers" }
            );

            context.MapRoute(
                name: null,
                url: "eusVote/topic/addComment",
                defaults: new { controller = "Topic", action = "Topic" },
                namespaces: new[] { "webUI.Areas.eusVote.Controllers" }
            );          

            context.MapRoute(
                name: null,
                url: "eusVote/topic/{action}/{id}",
                defaults: new { controller = "Topic", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "webUI.Areas.eusVote.Controllers" }
            );          
            
            context.MapRoute(
                name: null,
                url: "eusVote/{id}/{topicURL}",
                defaults: new { controller = "Topic",  action = "TopicAnswers"},
                namespaces: new[] { "webUI.Areas.eusVote.Controllers" }
            );

            //context.MapRoute(
            //    name: "eusVote",
            //    url: "eusVote/{controller}/{action}/{id}",
            //    defaults: new { controller = "Answers", action = "Details", id = UrlParameter.Optional },
            //    namespaces: new[] { "webUI.Areas.eusVote.Controllers" }
            //);
            
            context.MapRoute(
                "eusVote",
                "eusVote/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "webUI.Areas.eusVote.Controllers" }
            );
                       
            
        }
    }
}