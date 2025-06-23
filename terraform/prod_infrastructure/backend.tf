terraform {
  backend "azurerm" {
    resource_group_name  = "gamegather-rg"
    storage_account_name = "gamegatherst"
    container_name       = "dev-tfstate"
    key                 = "dev.terraform.tfstate"
    
  }
}