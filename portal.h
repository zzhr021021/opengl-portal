#pragma once
#include <glad/glad.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#define STB_IMAGE_IMPLEMENTATION

#include "mesh.h"
#include "shader.h"

#include <string>
#include <fstream>
#include <sstream>
#include <iostream>
#include <map>
#include <vector>
using namespace std;

class Portal
{
public:
    // object data
    string name;
    glm::vec3 position;
    glm::vec3 scale;
    float Yaw;
    float Pitch;
    Portal* twin;

    glm::mat4 RO;
    glm::vec3 AX;

    Portal(string name, glm::vec3 position, glm::vec3 scale, float yaw, float pitch) :
        name(name), position(position), scale(scale)
    {
        Yaw = yaw;
        Pitch = pitch;
        RO = glm::mat4(1.0);
        AX = glm::vec3(1.0, 0.0, 0.0);
    }
    void SetTwin(Portal* other) 
    {
        twin = other;
    }
    glm::mat4 GetModelMatrix()
    {
        glm::mat4 model = glm::mat4(1.0f);
        model = glm::translate(model, position);
        model = glm::rotate(model, glm::radians(Pitch), glm::vec3(glm::rotate(RO, glm::radians(Yaw), glm::vec3(0.0, 1.0, 0.0)) * glm::vec4(AX, 1.0f)));
        return glm::rotate(model, glm::radians(Yaw), glm::vec3(0.0, 1.0, 0.0));
    }
    glm::vec3 GetNewCameraPosition(glm::vec3 originalPos)
    {
        glm::mat4 rotation = glm::mat4(1.0);
        rotation = glm::rotate(rotation, glm::radians((*twin).Pitch - Pitch), glm::vec3(glm::rotate(RO, glm::radians((*twin).Yaw), glm::vec3(0.0, 1.0, 0.0)) * glm::vec4(AX, 1.0f)));
        rotation = glm::rotate(rotation, glm::radians((*twin).Yaw - Yaw), glm::vec3(0.0, 1.0, 0.0));
        glm::vec3 diff = glm::vec3(rotation * glm::vec4(originalPos - position, 1.0)) - (originalPos - position);

        glm::vec3 ret = originalPos;
        ret += (*twin).position - position;
        ret += diff;
        return ret;
    }

private:

};


