import React, { useRef } from "react";
import ReCAPTCHA from "react-google-recaptcha";

interface ReCAPTCHAFormProps {}

const ReCAPTCHAForm: React.FC<ReCAPTCHAFormProps> = (props) => {
  const recaptchaRef = useRef<ReCAPTCHA>(null);

  const onSubmitWithReCAPTCHA = async (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault(); // Prevent default form submission
    const token = await recaptchaRef.current?.executeAsync();
    // Now you can proceed with form submission or any other action
  };

  const handleClick = () => {
    recaptchaRef.current?.reset(); 
  };

  return (
    <form onSubmit={onSubmitWithReCAPTCHA}>
      <ReCAPTCHA
        ref={recaptchaRef}
        size="invisible"
        sitekey="6Le9zaYpAAAAAGCLVVxGarbEBQ3-fnkVgPyfbMXl"
      />
      <button type="submit" onClick={handleClick}>Submit</button>
    </form>
  );
};

export default ReCAPTCHAForm;
