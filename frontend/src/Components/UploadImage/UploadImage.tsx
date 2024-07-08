import React, {useState} from 'react';
import Compressor from 'compressorjs';

const Upload = ({setSelectedFile} : any ) => {  
  const handleCompressedUpload = (e: any) => {
    const image = e.target.files[0];
    new Compressor(image, {
      quality: 0.6,
      maxHeight: 850,
      maxWidth: 750,
      success: (compressedResult) => {
        setSelectedFile(compressedResult);
      },
    });
  };
  
  return (
    <>
    <h1>Upload Image</h1>
    <input
      type="file"
      id="myFile"
      name="filename"
      onChange={(e) => handleCompressedUpload(e)}
    />
    </>
  );
};

export default Upload;