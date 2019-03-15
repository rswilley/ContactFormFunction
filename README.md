# ContactFormFunction
Azure Functions reusable Contact Form Function App

*This README is a work in progress...*

I built this because I have multiple sites I manage with contact forms. 

Since a contact form is such a basic component of all websites I figured this would be a perfect use case for my first serverless function.

I wanted to be able to use a common sender email address for all my contact forms that utilized a reply to for the user sending the email.

I didn't want to have to verify multiple sender emails. I'm using Amazon SES, but you can use anything that supports SMTP.

## How to Use

POST the following JSON to the Azure Function's endpoint:

```json
{
  "fromDomain": "domain1.com",
  "name": "Sender's Name",
  "email": "sendersemail@theirdomain.com",
  "subject": "Email Subject",
  "message": "A great message."
}
```

FromDomain will match up with the ValidDomains environment variable.

Email will end up being a reply-to address.

### Environment variables required

- EmailHost - email server's hostname
- EmailPort - email server's port
- SmtpUsername - smtp username
- SmtpPassword - smtp password
- FromEmail - your verified sender email address (ex: mail@mycontactform.com)
- FromName - whatever you want the from name to be (ex: Contact Form)
- SubjectPrefix - email's subject prefix if you want one (ex: "[mysite.com] -" )
- ValidDomains - a list of valid domains with email addresses. format: domain1.com|recipient@domain1.com;domain2.com|recipient@domain2.com

## Final Email Example

```html
From: Contact Form <mail@mycontactform.com>
To: robert@rswilley.com
Reply-To: sendersemail@theirdomain.com
Date: Fri, 15 Mar 2019 10:31:00 +0000
Subject: [mysite.com] - Email Subject

<p>A great message.</p>
```
