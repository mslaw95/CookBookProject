import React, { useState } from 'react';
import styled from 'styled-components'

const Header = styled.h1`
    background: red;
    text-align: center;
`
const Form = styled.form`
    background: blue;
    text-align: center;
`
const NewRecipeForm = () => {
    const [title, setTitle] = useState('');

    const postRecipe = event => {
        // Prevents refreshing the page
        event.preventDefault();

        // Sends the request
        fetch('https://localhost:5001/recipes', {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
            },
            body: JSON.stringify({title: title})
        });

        // Clears the data
        setTitle('');
    }

    return (
        <div className="NewRecipeForm">
            <Header>New Recipe Form</Header>
                <Form onSubmit={postRecipe}>
                    <input id="title" name="title" type="text" onChange={event => setTitle(event.target.value)} value={title}/>
                    <input className="ingredients" type="text" defaultValue="Ingredients"/>
                    <input className="description" type="text" defaultValue="Description"/>
                    <input className="tags" type="text" defaultValue="Tags"/>
                    <button className="postRecipe" type="submit">Post</button>
                </Form>
        </div>
    );
}

export default NewRecipeForm;
