resource "azurerm_postgresql_flexible_server" "postgresql_server" {
  name                   = "${var.prefix_db}-server"
  resource_group_name    = var.resource_group_name
  location               = var.location
  version                = "16"
  administrator_login    = "postgres"
  administrator_password = "postgres"
  
  sku_name               = "B_Standard_B1ms"

  storage_mb             = 32768
  
  backup_retention_days  = 7
  zone                   = "1"
  
  geo_redundant_backup_enabled = false
}

resource "azurerm_postgresql_flexible_server_database" "postgresql_db" {
  name      = "${var.prefix_db}-database"
  server_id = azurerm_postgresql_flexible_server.postgresql_server.id
  collation = "en_US.utf8"
  charset   = "UTF8"
}

resource "azurerm_postgresql_flexible_server_firewall_rule" "allow_azure_services" {
  name                = "AllowAzureServicess"
  server_id           = azurerm_postgresql_flexible_server.postgresql_server.id
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "0.0.0.0"
}

resource "azurerm_postgresql_flexible_server_configuration" "ssl_off" {
  name      = "require_secure_transport"
  server_id = azurerm_postgresql_flexible_server.postgresql_server.id
  value     = "off"
}

resource "azurerm_service_plan" "asp" {
  name                = "${var.prefix_api}-asp"
  resource_group_name = var.resource_group_name
  location            = var.location
  os_type             = "Linux"
  sku_name            = "B1"
}

resource "azurerm_linux_web_app" "as" {
  name                = "${var.prefix_api}-webapp"
  resource_group_name = var.resource_group_name
  location            = var.location
  service_plan_id     = azurerm_service_plan.asp.id

  site_config {
    always_on = false
    application_stack {
        dotnet_version = "8.0"
    }
    cors {
      allowed_origins = ["*"]
    }
  }

  app_settings = {
    "ConnectionStrings__Default" = "Server=${azurerm_postgresql_flexible_server.postgresql_server.fqdn};Port=5432;Database=${azurerm_postgresql_flexible_server_database.postgresql_db.name};Username=${azurerm_postgresql_flexible_server.postgresql_server.administrator_login};Password=${azurerm_postgresql_flexible_server.postgresql_server.administrator_password};SSL Mode=Require;"
  }

}