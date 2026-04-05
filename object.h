#pragma once
#include <glad/glad.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#define STB_IMAGE_IMPLEMENTATION

#include "mesh.h"
#include "shader.h"
#include "model.h"

#include <string>
#include <fstream>
#include <sstream>
#include <iostream>
#include <map>
#include <vector>
using namespace std;

class Object
{
public:
    // object data
    string name;
    Model* model;
    glm::vec3 position;
    glm::vec3 scale;
    glm::vec3 rotation;

    // constructor, expects a 3D model pointer.
    Object(string name, Model * model, glm::vec3 position, glm::vec3 scale, glm::vec3 rotation) : name(name), model(model), position(position), scale(scale), rotation(rotation)
    {
    }

    void Draw() 
    {

    }

private:
    
};


