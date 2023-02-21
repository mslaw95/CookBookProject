import React from 'react';
import styled from 'styled-components'

const Menu = styled.h1`
    background: yellow;
`
const Header = styled.h1`
    background: red;
    text-align: center;
`
const Form = styled.form`
    background: blue;
    text-align: center;
`
const RecipeList = () => {  
    const getRecipes = event => {
        event.preventDefault();
        fetch(
        'https://localhost:5001/recipes', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept'
          },}
      );
    }
  
    return (
        <div className="RecipeList">
            <Menu>Menu Component TBD</Menu>
            <Header>Recipe List</Header>
                <Form onSubmit={getRecipes}>
                    <input className="search-bar" type="text" value="Search"/>
                    <button className="search-button" type="submit">Search</button>
                </Form>
        </div>
    );
}

export default RecipeList;
