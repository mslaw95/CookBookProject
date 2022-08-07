import React from "react";
import styled from 'styled-components'

const RecipeComponent = styled.div`
    background: green;
    text-align: center;
`

const Recipe = (title) => {
    return(
        <RecipeComponent>
            <h1>{title}</h1>
        </RecipeComponent>
    );
};

export default Recipe;