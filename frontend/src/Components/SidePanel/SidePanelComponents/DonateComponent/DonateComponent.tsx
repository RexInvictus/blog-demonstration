import React from "react";
import KoFi from "./Ko-Fi.png";
import PayPal from "./PayPal.png";
import Bitcoin from "./Bitcoin.png";
import "./DonateComponent.css";

interface Props {}

const DonateComponent = (props: Props) => {
  return (
    <div className="sidepanel-box">
      <div>
        <h3>Donate</h3>
        <ul className="donate-list">
          <li>
            <a href="https://ko-fi.com/lithuanian">
              <img src={KoFi} height={30} alt="Ko-Fi" />
              <h2>Ko-Fi</h2>
            </a>
          </li>
          <li>
            <a href="https://www.paypal.com/paypalme/ModestasLukauskas">
              <img src={PayPal} height={30} alt="PayPal" />
              <h2>PayPal</h2>
            </a>
          </li>
        </ul>

        <p className="donate-text">
          Thank you for your generosity. Donations are much appreciated.
        </p>
      </div>
    </div>
  );
};

export default DonateComponent;
