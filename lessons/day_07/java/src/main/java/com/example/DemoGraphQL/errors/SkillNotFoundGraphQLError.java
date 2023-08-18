package com.example.DemoGraphQL.errors;

import graphql.ErrorClassification;
import graphql.ErrorType;
import graphql.GraphQLError;
import graphql.language.SourceLocation;
import java.util.Collections;
import java.util.List;
import java.util.Map;

public class SkillNotFoundGraphQLError extends RuntimeException implements GraphQLError {

    private String invalidField;

    public SkillNotFoundGraphQLError(String message, String invalidField) {
        super(message);
        this.invalidField = invalidField;
    }

    @Override
    public String getMessage() {
        return super.getMessage();
    }

    @Override
    public List<Object> getPath() {
        return null;
    }

    @Override
    public List<SourceLocation> getLocations() {
        return null;
    }

    @Override
    public ErrorClassification getErrorType() {
        return ErrorType.ValidationError;
    }

    @Override
    public Map<String, Object> getExtensions() {
        return Collections.singletonMap("invalidField", invalidField);
    }
}
