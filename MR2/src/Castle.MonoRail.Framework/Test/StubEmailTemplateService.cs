// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.MonoRail.Framework.Test
{
	using System.Collections;
	using System.Net.Mail;
	using Core;

	/// <summary>
	/// Mocks the <see cref="IEmailTemplateService"/> calling 
	/// <see cref="StubEngineContext.AddMailTemplateRendered"/> to register
	/// the calls made to these methods
	/// </summary>
	public class StubEmailTemplateService : IEmailTemplateService
	{
		private readonly StubEngineContext context;

		/// <summary>
		/// Initializes a new instance of the <see cref="StubEmailTemplateService"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public StubEmailTemplateService(StubEngineContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Determines whether the specified template exists in the e-mail
		/// template folder (<c>views/mail</c>).
		/// </summary>
		/// <param name="templateName">Name of the e-mail template.</param>
		/// <returns>
		/// 	<c>true</c> if the template exists; otherwise, <c>false</c>.
		/// </returns>
		public bool HasMailTemplate(string templateName)
		{
			return true;
		}

		/// <summary>
		/// Creates an instance of <see cref="MailMessage"/>
		/// using the specified template for the body
		/// </summary>
		/// <param name="templateName">Name of the template to load.
		/// Will look in <c>Views/mail</c> for that template file.</param>
		/// <param name="layoutName">Name of the layout.</param>
		/// <param name="parameters">Dictionary with parameters
		/// that you can use on the email template</param>
		/// <returns>An instance of <see cref="MailMessage"/></returns>
		public MailMessage RenderMailMessage(string templateName, string layoutName, IDictionary parameters)
		{
			context.AddMailTemplateRendered(templateName, parameters);

			return new MailMessage("from@castle.org", "to@castle.org", "subject", "body");
		}

		/// <summary>
		/// Creates an instance of <see cref="MailMessage"/>
		/// using the specified template for the body
		/// </summary>
		/// <param name="templateName">Name of the template to load.
		/// Will look in <c>Views/mail</c> for that template file.</param>
		/// <param name="layoutName">Name of the layout.</param>
		/// <param name="parameters">Dictionary with parameters
		/// that you can use on the email template</param>
		/// <returns>An instance of <see cref="MailMessage"/></returns>
		public MailMessage RenderMailMessage(string templateName, string layoutName, object parameters)
		{
			context.AddMailTemplateRendered(templateName, new ReflectionBasedDictionaryAdapter(parameters));

			return new MailMessage("from@castle.org", "to@castle.org", "subject", "body");
		}

		/// <summary>
		/// Renders the mail message.
		/// </summary>
		/// <param name="templateName">Name of the template.</param>
		/// <param name="engineContext">The engine context.</param>
		/// <param name="controller">The controller.</param>
		/// <param name="controllerContext">The controller context.</param>
		/// <param name="doNotApplyLayout">if set to <c>true</c> [do not apply layout].</param>
		/// <returns></returns>
		public MailMessage RenderMailMessage(string templateName, IEngineContext engineContext, IController controller,
		                                 IControllerContext controllerContext, bool doNotApplyLayout)
		{
			context.AddMailTemplateRendered(templateName, controllerContext.PropertyBag);

			return new MailMessage("from@castle.org", "to@castle.org", "subject", "body");
		}
	}
}