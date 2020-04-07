import React, { useState, useEffect } from 'react';

export default function Home(props) {
    const [value, setValue] = useState('');
    const [images, setImages] = useState([]);
    return (
        <div>
            <form>
                <input list="image_list" />
                <datalist id="image_list" onChange={({ target: { value } }) => setValue(value)} value={value}>
                    <select onChange={({ target: { value } }) => setValue(value)}>
                        <option value="">Search images you want</option>
                        {images.map(image =>
                            (
                                <option key={image.id}>{image.Name}</option>
                            )
                        )}
                    </select>
                </datalist>
            </form>
      </div>
    )
}
