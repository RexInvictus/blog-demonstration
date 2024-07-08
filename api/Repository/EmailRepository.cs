using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using api.Interfaces;
using api.Models;

namespace api.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _config;

        public EmailRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string to, string subject, string html)
        {
            using (var client = new AmazonSimpleEmailServiceClient(_config["AwsConfiguration:SmtpUsername"], _config["AwsConfiguration:SmtpPassword"], Amazon.RegionEndpoint.EUWest2))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = _config["AwsConfiguration:FromSmtp"],
                    Destination = new Destination { ToAddresses = { to } },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Html = new Content(html)
                        }

                    }
                };
                await client.SendEmailAsync(sendRequest);
            };

        }


        public async Task SendToAllAsync(List<Subscriber> subscribers, string title, int id)
        {
            await SendEmailAsync("lietuvisanglijoje@gmail.com", "Sent Out Notification Email", $"<html><body><p>Sent out email about your new post to {subscribers.ToArray().Length} subscribers.</p></body></html>");
            // iterate through subscribers
            foreach (var subscriber in subscribers)
            {
                string toEmail = subscriber.Email;
                string toName = subscriber.Name;

                string subject = $"Modestas Travels | {title}";
                string htmlContent = $"<html><body style='background-color:#f3f0ec'><div style='height: 30px;'></div><div style='max-width:max-content;margin:auto;background-color:#fff;height:100%;border-radius:10px;box-shadow:0 4px 8px rgba(0,0,0,.5)'><div style='margin-left:20px;width:90%'><div style='height:1px'></div><h1>Dear {toName},</h1><h3>I just published a new post on my blog, and I couldn't wait to share it with you!</h3><h3>Click below to read:</h3><h1><a href='www.modestastravels.com/blog/{id}' target='_blank'>{title}</a></h1><div style='border-top:1px solid grey'><table cellpadding='0' cellspacing='0' class='table__StyledTable-sc-1avdl6r-0 kAbRZI' style='min-width:450px;vertical-align:-webkit-baseline-middle;font-size:medium;font-family:Arial'><tbody><tr style='text-align:left'><td><div style='display:flex'><img src='https://i.ibb.co/VBGmLS2/logo192.jpg' role='presentation' width='130' class='image__StyledImage-sc-hupvqm-0 gYgOut' style='display:inline-block;max-width:130px'><div style='margin-top:20px;margin-left:20px'><h2 color='#000000' class='name__NameContainer-sc-1m457h3-0 jxbGUj' style='margin:0;font-size:18px;color:#000;font-weight:600'><span>Modestas Lukauskas</span></h2><p color='#000000' font-size='medium' class='job-title__Container-sc-1hmtp73-0 ifJNJc' style='margin:0;color:#000;font-size:14px;line-height:22px'><span>Blogger</span></p><p color='#000000' font-size='medium' class='company-details__CompanyContainer-sc-j5pyy8-0 VnOLK' style='margin:0;font-weight:500;color:#000;font-size:14px;line-height:22px'><span>Modestas Travels</span></p></div></div></td></tr><tr><td><table cellpadding='0' cellspacing='0' class='table__StyledTable-sc-1avdl6r-0 kAbRZI' style='width:100%;vertical-align:-webkit-baseline-middle;font-size:medium;font-family:Arial'><tbody><tr><td color='#f86295' direction='horizontal' width='auto' height='1' class='color-divider__Divider-sc-1h38qjv-0 llIisW' style='width:100%;border-bottom:1px solid #f86295;border-left:none;display:block'></td></tr><tr><td height='10'></td></tr><tr style='vertical-align:middle'><td><table cellpadding='0' cellspacing='0' class='table__StyledTable-sc-1avdl6r-0 kAbRZI' style='vertical-align:-webkit-baseline-middle;font-size:medium;font-family:Arial'><tbody><tr height='25' style='vertical-align:middle'><td width='30' style='vertical-align:middle'><table cellpadding='0' cellspacing='0' class='table__StyledTable-sc-1avdl6r-0 kAbRZI' style='vertical-align:-webkit-baseline-middle;font-size:medium;font-family:Arial'><tbody><tr><td style='vertical-align:bottom'><span color='#f86295' width='11' class='contact-info__IconWrapper-sc-mmkjr6-1 bglVXe' style='display:inline-block;background-color:#f86295'><img src='https://cdn2.hubspot.net/hubfs/53/tools/email-signature-generator/icons/email-icon-2x.png' color='#f86295' alt='emailAddress' width='13' class='contact-info__ContactLabelIcon-sc-mmkjr6-0 cnkwri' style='display:block;background-color:#f86295'></span></td></tr></tbody></table></td><td style='padding:0'><a href='mailto:lietuvisanglijoje@gmail.com' color='#000000' class='contact-info__ExternalLink-sc-mmkjr6-2 ibLXSU' style='text-decoration:none;color:#000;font-size:12px'><span>lietuvisanglijoje@gmail.com</span></a></td></tr><tr height='25' style='vertical-align:middle'><td width='30' style='vertical-align:middle'><table cellpadding='0' cellspacing='0' class='table__StyledTable-sc-1avdl6r-0 kAbRZI' style='vertical-align:-webkit-baseline-middle;font-size:medium;font-family:Arial'><tbody><tr><td style='vertical-align:bottom'><span color='#f86295' width='11' class='contact-info__IconWrapper-sc-mmkjr6-1 bglVXe' style='display:inline-block;background-color:#f86295'><img src='https://cdn2.hubspot.net/hubfs/53/tools/email-signature-generator/icons/link-icon-2x.png' color='#f86295' alt='website' width='13' class='contact-info__ContactLabelIcon-sc-mmkjr6-0 cnkwri' style='display:block;background-color:#f86295'></span></td></tr></tbody></table></td><td style='padding:0'><a href='//www.modestastravels.com' color='#000000' class='contact-info__ExternalLink-sc-mmkjr6-2 ibLXSU' style='text-decoration:none;color:#000;font-size:12px'><span>www.modestastravels.com</span></a></td></tr></tbody></table></td><td style='text-align:right'><table cellpadding='0' cellspacing='0' class='table__StyledTable-sc-1avdl6r-0 kAbRZI' style='width:100%;vertical-align:-webkit-baseline-middle;font-size:medium;font-family:Arial'><tbody><tr><td><table cellpadding='0' cellspacing='0' class='table__StyledTable-sc-1avdl6r-0 kAbRZI' style='display:inline-block;vertical-align:-webkit-baseline-middle;font-size:medium;font-family:Arial'><tbody><tr style='text-align:right'><td><a href='https://www.facebook.com/modestas.lukauskas.1' color='#7075db' class='social-links__LinkAnchor-sc-py8uhj-2 hBVWAh' style='display:inline-block;padding:0;background-color:#7075db'><img src='https://cdn2.hubspot.net/hubfs/53/tools/email-signature-generator/icons/facebook-icon-2x.png' alt='facebook' color='#7075db' width='24' class='social-links__LinkImage-sc-py8uhj-1 hSTSwA' style='background-color:#7075db;max-width:135px;display:block'></a></td><td width='5'><div></div></td><td><a href='https://www.instagram.com/lithuaniaunderground/' color='#7075db' class='social-links__LinkAnchor-sc-py8uhj-2 hBVWAh' style='display:inline-block;padding:0;background-color:#7075db'><img src='https://cdn2.hubspot.net/hubfs/53/tools/email-signature-generator/icons/instagram-icon-2x.png' alt='instagram' color='#7075db' width='24' class='social-links__LinkImage-sc-py8uhj-1 hSTSwA' style='background-color:#7075db;max-width:135px;display:block'></a></td><td width='5'><div></div></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td height='10'></td></tr><tr><td color='#f86295' direction='horizontal' width='auto' height='1' class='color-divider__Divider-sc-1h38qjv-0 llIisW' style='width:100%;border-bottom:1px solid #f86295;border-left:none;display:block'></td></tr><tr><td height='30'></td></tr></tbody></table></td></tr></tbody></table></div></div></div><div style='height: 30px;'></div></body></html>";

                try
                {
                    await SendEmailAsync(toEmail, subject, htmlContent);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

    }
}