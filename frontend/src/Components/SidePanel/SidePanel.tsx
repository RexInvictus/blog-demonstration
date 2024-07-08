import React from "react";
import AboutComponent from "./SidePanelComponents/AboutComponent/AboutComponent";
import "./SidePanel.css";
import DonateComponent from "./SidePanelComponents/DonateComponent/DonateComponent";
import CommentComponent from "./SidePanelComponents/CommentComponent/CommentComponent";
import SubscribeComponent from "./SidePanelComponents/SubscribeComponent/SubscribeComponent";

interface Props {
  comment: any;
  stretch: boolean;
}

const SidePanel = (props: Props) => {
  return (
    <div
      className={
        props.stretch ? "sidepanel-container stretch" : "sidepanel-container"
      }
    >
      <AboutComponent />
      <SubscribeComponent />
      {props.comment && <CommentComponent comments={props.comment} />}
      <DonateComponent />
    </div>
  );
};

export default SidePanel;
