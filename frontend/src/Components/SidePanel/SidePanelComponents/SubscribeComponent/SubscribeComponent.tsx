import React, { useRef, useState } from "react";
import "./SubscribeComponent.css";
import axios from "axios";
import ReCAPTCHA from "react-google-recaptcha";
import { apiUrl } from "../../../../config";
interface Props {}


const SubscribeComponent = (props: Props) => {
  const [email, setEmail] = useState<string>("");
  const [name, setName] = useState<string>("");
  const [subscribed, setSubscribed] = useState<boolean>(false);
  const recaptchaRef = useRef<ReCAPTCHA>(null);

  const handleSubscribe = async (t: any) => {
    try {
      setSubscribed(true);

      await axios.post(`${apiUrl}/api/subscriber`, {
        name: name,
        email: email,
        captchaToken: t,
      });
    } catch {
      // do nothing
    }
  };
  return (
    <div className="sidepanel-box">
      <h3 style={{ marginBottom: 0 }}>Subscribe</h3>
      {subscribed ? (
        <p style={{ textAlign: "center" }}>Subscribed!</p>
      ) : (
        <div>
          <p style={{ margin: 0, opacity: 0.5, width: "90%", marginLeft: 10 }}>
            Subscribe to my blog for updates on my latest adventures.
          </p>
          <div className="subscribe-form-subscribe">
            <input
              type="text"
              placeholder="Name"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
            <input
              type="text"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
            <form
              onSubmit={(e) => {
                try {
                e.preventDefault();
                recaptchaRef.current?.execute();
                }
                catch
                {}
              }}
            >
              <ReCAPTCHA
                ref={recaptchaRef}
                size="invisible"
                sitekey="6LeiyKYpAAAAAKh_7K2vb1BZlNrw8GrsTIP0rV7n"
                onChange={(t) => {
                  handleSubscribe(t);
                }}
              />
              <button type="submit">Subscribe</button>
            </form>
          </div>
        </div>
      )}
      <br />
    </div>
  );
};

export default SubscribeComponent;
